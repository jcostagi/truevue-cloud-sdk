/*******************************************************************************
 * Copyright (c) 2020 Sensormatic Electronics, LLC.  All rights reserved.
 * Reproduction is forbidden without written approval of Sensormatic Electronics, LLC.
 *******************************************************************************/

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TrueVUE.Cloud.SDK.Extensions
{
    public static class MultiPartFormDataContentExtensions
    {
        public static void Add(this MultipartFormDataContent form, HttpContent content, Dictionary<string, string> formValues)
        {
            Add(form, content, formValues);
        }

        public static void Add(this MultipartFormDataContent form, HttpContent content, string name, Dictionary<string, string> formValues)
        {
            Add(form, content, formValues, name: name);
        }

        public static void Add(this MultipartFormDataContent form, HttpContent content, string name, string fileName, Dictionary<string, string> formValues)
        {
            Add(form, content, formValues, name: name, fileName: fileName);
        }

        static void Add(this MultipartFormDataContent form, HttpContent content, Dictionary<string, string> formValues, string name = null, string fileName = null)
        {
            var header = new ContentDispositionHeaderValue("form-data");
            header.Name = name;
            header.FileName = fileName;
            header.FileNameStar = fileName;

            foreach (var parameter in formValues.Keys)
            {
                header.Parameters.Add(new NameValueHeaderValue(parameter, formValues[parameter]));
            }

            content.Headers.ContentDisposition = header;
            form.Add(content);
        }
    }
}
