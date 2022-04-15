﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Contrast.K8s.AgentOperator.Core.Kube;
using DotnetKubernetesClient;
using JsonDiffPatch;
using k8s;
using k8s.Models;
using Newtonsoft.Json;
using NLog;

namespace Contrast.K8s.AgentOperator.Core
{
    public interface IResourcePatcher
    {
        Task Patch<T>(T entity, Action<T> mutator) where T : IKubernetesObject<V1ObjectMeta>;
    }

    public class ResourcePatcher : IResourcePatcher
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly JsonDiffer _jsonDiffer;
        private readonly IKubernetesClient _client;
        private readonly KubernetesJsonSerializer _jsonSerializer;

        public ResourcePatcher(JsonDiffer jsonDiffer, IKubernetesClient client, KubernetesJsonSerializer jsonSerializer)
        {
            _jsonDiffer = jsonDiffer;
            _client = client;
            _jsonSerializer = jsonSerializer;
        }

        public async Task Patch<T>(T entity, Action<T> mutator) where T : IKubernetesObject<V1ObjectMeta>
        {
            var entityCopy = _jsonSerializer.DeepClone(entity);

            var currentVersion = _jsonSerializer.ToJToken(entityCopy);
            mutator.Invoke(entityCopy);
            var nextVersion = _jsonSerializer.ToJToken(entityCopy);

            var diff = _jsonDiffer.Diff(currentVersion, nextVersion, false);
            if (diff.Operations.Any())
            {
                Logger.Debug(
                    $"Peparing to patch '{entity.Namespace()}/{entity.Name()}' ('{entity.Kind}/{entity.ApiVersion}') with '{diff.ToString(Formatting.None)}'.");

                await _client.Patch(entity, diff);

                Logger.Debug("Patch complete.");
            }
        }
    }
}