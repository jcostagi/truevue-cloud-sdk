/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System;
using System.Collections.Generic;

namespace TrueVUE.Cloud.SDK.Epc.Interfaces
{
    public interface IFilter : IEquatable<IFilter>
    {
        string Name { get; }

        string Id { get; }

        string Type { get; }

        List<string> SelectedOptions { get; }

		bool Include { get; }

		bool IsBlob { get; }

        bool IsRecipe { get; }
    }
}
