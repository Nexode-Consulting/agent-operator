﻿using System.Text.Json.Serialization;
using JetBrains.Annotations;
using k8s.Models;
using KubeOps.Operator.Entities;

namespace Contrast.K8s.AgentOperator.Core.OpenShift
{
    [KubernetesEntity(Group = "apps.openshift.io", ApiVersion = "v1", Kind = "DeploymentConfig", PluralName = "deploymentconfigs"), UsedImplicitly]
    public class V1DeploymentConfig : CustomKubernetesEntity<V1DeploymentConfig.DeploymentConfigSpec>
    {
        /// <summary>
        /// DeploymentConfigSpec represents the desired state of the deployment.
        /// </summary>
        public class DeploymentConfigSpec
        {
            /// <summary>
            /// Selector is a label query over pods that should match the Replicas count.
            /// </summary>
            [JsonPropertyName("selector")]
            public V1LabelSelector? Selector { get; set; }

            /// <summary>
            /// Template is the object that describes the pod that will be created if insufficient replicas are detected.
            /// </summary>
            [JsonPropertyName("template")]
            public V1PodTemplateSpec? Template { get; set; }
        }
    }
}
