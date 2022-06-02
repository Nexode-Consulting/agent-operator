﻿using Contrast.K8s.AgentOperator.Core.State.Resources;

namespace Contrast.K8s.AgentOperator.Core.Reactions.Injecting.Patching
{
    public record PatchingContext(AgentInjectorResource Injector,
                                  AgentConnectionResource Connection,
                                  AgentConfigurationResource? Configuration,
                                  string ContrastMountPath);
}