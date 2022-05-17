﻿using System;

namespace Contrast.K8s.AgentOperator.Core.Telemetry
{
    public record ClusterId(Guid Guid, DateTimeOffset CreatedOn)
    {
        public static ClusterId NewId()
        {
            return new ClusterId(Guid.NewGuid(), DateTimeOffset.Now);
        }
    }
}
