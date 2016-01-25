using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WebRole1
{
    /// <summary>
    /// Summary description for GetOneBlob
    /// </summary>
    public class GetBlobs : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";


            //--Connection String --

            //storage account
            CloudStorageAccount objStorage = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("BlobConnectionString"));
            // get the Client reference using storage blob end point
            CloudBlobClient objClient = new CloudBlobClient(objStorage.BlobEndpoint, objStorage.Credentials);

            // Get Container reference

            CloudBlobContainer blobContainer = objClient.GetContainerReference("mycontainer");
            context.Response.ContentType = "application/json";
            // Create the container if it does not exist
            blobContainer.CreateIfNotExists();
            //set public permission
            blobContainer.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
            List<object> imageUriList = new List<object>();
            //list blobs in the container
            IEnumerable<IListBlobItem> objBlobList = blobContainer.ListBlobs();
            foreach (IListBlobItem objItem in objBlobList)
            {
                var imageUri = new
                {
                    ImageURI = objItem.Uri
                };
               imageUriList.Add(imageUri);
            }
            var javaScriptSerializer = new JavaScriptSerializer();
            context.Response.Write(javaScriptSerializer.Serialize(imageUriList));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}