/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

namespace TrueVUE.Cloud.SDK.Epc.Models
{
    class EpcFieldDefinition
    {
        #region Constructors

        public EpcFieldDefinition(string fieldName, int minLength, int maxLength, string description, bool isRequired)
        {
            FieldName = fieldName;
            MinLength = minLength;
            MaxLength = maxLength;
            Description = description;
            IsRequired = isRequired;
        }

        #endregion

        #region Properties

        public string FieldName { get; private set; }

        public int MinLength { get; private set; }

        public int MaxLength { get; private set; }

        public string Description { get; private set; }

        public bool IsRequired { get; private set; }

        #endregion
    }
}
