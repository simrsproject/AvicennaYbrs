using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.Common.Inacbg.v54
{
    public class File
    {
        public class Upload
        {
            public class Metadata
            {
                public string NomorSep { get; set; }

                public string FileClass { get; set; }

                public string FileName { get; set; }
            }

            public class RootObject
            {
                public Metadata Metadata { get; set; }

                public string Data { get; set; }
            }

            public class UploadResponse
            {
                public class Response
                {
                    [JsonProperty("file_id")]
                    public string FileId { get; set; }

                    [JsonProperty("file_name")]
                    public string FileName { get; set; }

                    [JsonProperty("file_type")]
                    public string FileType { get; set; }

                    [JsonProperty("file_size")]
                    public string FileSize { get; set; }

                    [JsonProperty("file_class")]
                    public string FileClass { get; set; }
                }

                public class UploadDcBpjsResponse
                {
                    [JsonProperty("metaData")]
                    public Inacbg.Metadata Metadata { get; set; }
                }

                public class Metadata : Inacbg.Metadata
                {
                    [JsonProperty("error_no")]
                    public string ErrorNo { get; set; }

                    [JsonProperty("response")]
                    public Response Response { get; set; }

                    [JsonProperty("upload_dc_bpjs_response")]
                    public UploadDcBpjsResponse UploadDcBpjsResponse { get; set; }
                }

                public class Result
                {
                    [JsonProperty("metadata")]
                    public Metadata Metadata { get; set; }

                    [JsonProperty("response")]
                    public Response Response { get; set; }
                }
            }
        }

        public class Delete
        {
            public class Data
            {
                public string NomorSep { get; set; }

                public string FileId { get; set; }
            }

            public class RootObject
            {
                public Data Data { get; set; }
            }
        }

        public class Get
        {
            public class Data
            {
                public string NomorSep { get; set; }
            }

            public class RootObject
            {
                public Data Data { get; set; }
            }

            public class UploadDcBpjsResponseResponse
            {
                [JsonProperty("keterangan")]
                public string Keterangan { get; set; }
            }
            
            public class UploadDcBpjsResponse
            {
                [JsonProperty("metadata")]
                public Inacbg.Metadata Metadata { get; set; }

                [JsonProperty("response")]
                public UploadDcBpjsResponseResponse Response { get; set; }
            }

            public class DataResponse
            {
                [JsonProperty("file_id")]
                public string FileId { get; set; }

                [JsonProperty("file_name")]
                public string FileName { get; set; }

                [JsonProperty("file_size")]
                public string FileSize { get; set; }

                [JsonProperty("file_type")]
                public string FileType { get; set; }

                [JsonProperty("file_class")]
                public string FileClass { get; set; }

                [JsonProperty("upload_dc_bpjs")]
                public string UploadDcBpjs { get; set; }

                [JsonProperty("upload_dc_bpjs_response")]
                public UploadDcBpjsResponse UploadDcBpjsResponse { get; set; }
            }

            public class FileResponse
            {
                [JsonProperty("count")]
                public int Count { get; set; }

                [JsonProperty("data")]
                public DataResponse[] DataResponse { get; set; }
            }

            public class Response
            {
                [JsonProperty("metadata")]
                public Inacbg.Metadata Metadata { get; set; }

                [JsonProperty("response")]
                public FileResponse FileResponse { get; set; }
            }
        }
    }
}