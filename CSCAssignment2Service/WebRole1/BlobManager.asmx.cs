using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace WebRole1
{
    /// <summary>
    /// Summary description for GetBlobs1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [ScriptService]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BlobManager : System.Web.Services.WebService
    {

        [WebMethod]
  
        public object GetBlobList()
        {
            


            //--Connection String --

            //storage account
            CloudStorageAccount objStorage = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("BlobConnectionString"));
            // get the Client reference using storage blob end point
            CloudBlobClient objClient = new CloudBlobClient(objStorage.BlobEndpoint, objStorage.Credentials);

            // Get Container reference

            CloudBlobContainer blobContainer = objClient.GetContainerReference("mycontainer");
            
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
            //var javaScriptSerializer = new JavaScriptSerializer();
           //return javaScriptSerializer.Serialize(imageUriList);
            return imageUriList;
        }

        [WebMethod]
        public string UploadFile(byte[] pic, int length,string fileName)
        {
            try
            {
                //--Connection String --

                //storage account
                CloudStorageAccount objStorage = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("BlobConnectionString"));
                // get the Client reference using storage blob end point
                CloudBlobClient objClient = new CloudBlobClient(objStorage.BlobEndpoint, objStorage.Credentials);

                // Get Container reference

                CloudBlobContainer blobContainer = objClient.GetContainerReference("mycontainer");

                // Create the container if it does not exist
                blobContainer.CreateIfNotExists();
                //set public permission
                blobContainer.SetPermissions(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);
                blockBlob.UploadFromByteArray(pic, 0, length);

               
                

                string blobUrl = blockBlob.Uri.AbsoluteUri;

                return blobUrl;
            }
            catch {
                return "Fail";
            }
        }
    }
}
