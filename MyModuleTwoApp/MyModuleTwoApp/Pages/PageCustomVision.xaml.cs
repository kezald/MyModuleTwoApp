using MyModuleTwoApp.Model;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyModuleTwoApp
{
    public partial class PageCustomVision : ContentPage
    {
        public PageCustomVision()
        {
            InitializeComponent();
        }

        async private void loadCamera(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Camera", "No camera is present.", "OK");
            }
            else
            {
                MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                    Directory = "Sample",
                    Name = $"{DateTime.UtcNow}.jpg"
                });

                if (file != null)
                {
                    image.Source = ImageSource.FromStream(() =>
                    {
                        return file.GetStream();
                    });

                    await MakePredictionRequest(file);
                }
            }
        }

        static byte[] GetImageAsByteArray(MediaFile file)
        {
            Stream stream = file.GetStream();
            BinaryReader binaryReader = new BinaryReader(stream);
            return binaryReader.ReadBytes((int)stream.Length);
        }

        async Task MakePredictionRequest(MediaFile file)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Prediction-Key", "c11b49980c4f42e0a34b8820a0607713");

            string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v1.0/Prediction/d094d72a-8e5e-48e5-bfb1-d5b8be2f070c/image?iterationId=ec824ab3-e526-4b65-9662-b69f1069954a";

            HttpResponseMessage response;

            byte[] byteData = GetImageAsByteArray(file);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                TagLabel.Text = "Processing...";
                response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();

                    EvaluationModel responseModel = JsonConvert.DeserializeObject<EvaluationModel>(responseString);

                    String predictionData = "";
                    Prediction mostProbable = null;
                    foreach (Prediction prediction in responseModel.Predictions)
                    {
                        predictionData += prediction.Tag + " with probability " + prediction.Probability + "\n";
                        if (mostProbable == null || mostProbable.Probability < prediction.Probability)
                        {
                            mostProbable = prediction;
                        }
                    }


                    TagLabel.Text = (mostProbable != null && mostProbable.Probability > 0.5d) ?
                        "It is a " + mostProbable.Tag + "!" :
                        "No result";
                    TagLabel.Text = TagLabel.Text + "\n\n" + predictionData;

                    file.Dispose();
                }
                else
                {
                    TagLabel.Text = "Operation Failed! " + response.StatusCode;
                }
            }
        }
    }
}
