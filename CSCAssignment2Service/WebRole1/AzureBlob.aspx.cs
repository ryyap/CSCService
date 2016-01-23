using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebRole1
{
    public partial class AzureBlob : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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

            //list blobs in the container
            IEnumerable<IListBlobItem> objBlobList = blobContainer.ListBlobs();
            foreach (IListBlobItem objItem in objBlobList)
            {
                Response.Write("<img src='" + objItem.Uri + "'/>");
                Response.Write("<br>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
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

            if (!FileUpload1.HasFile)
            {
                Label1.Visible = true;
                Label1.Text = "Please Select Image File";
                //checking if file uploader has no file selected


            }
            else
            {
                // Get blob reference 
                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(FileUpload1.FileName.ToString());

                int length = FileUpload1.PostedFile.ContentLength;
                byte[] pic = new byte[length];


                FileUpload1.PostedFile.InputStream.Read(pic, 0, length);

                // Create or overwrite the "myblob" blob with contents from a local file.

                blockBlob.UploadFromByteArray(pic, 0, length);

            }
        }
    }
}