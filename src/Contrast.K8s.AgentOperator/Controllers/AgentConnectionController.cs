﻿using Contrast.K8s.AgentOperator.Core;
using Contrast.K8s.AgentOperator.Entities;
using JetBrains.Annotations;
using KubeOps.Operator.Rbac;

namespace Contrast.K8s.AgentOperator.Controllers
{
    [EntityRbac(typeof(V1Beta1AgentConnection), Verbs = VerbConstants.ReadAndPatch), UsedImplicitly]
    public class AgentConnectionController : GenericController<V1Beta1AgentConnection>
    {
        public AgentConnectionController(IEventStream eventStream) : base(eventStream)
        {
        }
    }
}