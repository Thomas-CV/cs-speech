using Microsoft.CognitiveServices.Speech;
using NAudio.Wave;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CognitiveServicesSpeech.CognitiveServices
{
	public class Speech
	{
		#region Properties and Constructor
		private string SubscriptionKey { get; set; }
		private string ApiRegion { get; set; }
		private string AuthenticationApi { get; set; }
		private string SpeechApi { get; set; }
		private string AccessToken { get; set; }

		public Speech(string subscriptionKey, string apiRegion, string authenticationApi, string speechApi)
		{
			SubscriptionKey = subscriptionKey;
			ApiRegion = apiRegion;
			AuthenticationApi = authenticationApi;
			SpeechApi = speechApi;
		}
		#endregion

		#region Authenticate
		public async Task AuthenticateAsync()
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);
				UriBuilder uriBuilder = new UriBuilder(AuthenticationApi);

				var result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null).ConfigureAwait(false);
				AccessToken = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
			}
		}
		#endregion


		#region Speech to Text
		public async Task<string> RecognizeSpeechAsync()
		{
			// Prepare configuration
			var config = SpeechConfig.FromSubscription(SubscriptionKey, ApiRegion);

			// Recognize speech
			using (var recognizer = new SpeechRecognizer(config))
			{
				var result = await recognizer.RecognizeOnceAsync();

				// Return recognized text
				if (result.Reason == ResultReason.RecognizedSpeech) return result.Text;
			}
			// Return null on error or unrecognized
			return null;
		}
		#endregion

		#region Text to Speech
		public async Task SpeakAsync(string text, string voice)
		{
			using (var client = new HttpClient())
			{
				using (var request = new HttpRequestMessage())
				{
					request.Method = HttpMethod.Post;
					request.RequestUri = new Uri(SpeechApi);
					request.Content = new StringContent(string.Format("<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'><voice name='{0}'>{1}</voice></speak>", voice, text), Encoding.UTF8, "application/ssml+xml");
					request.Headers.Add("Authorization", "Bearer " + AccessToken);
					request.Headers.Add("Connection", "Keep-Alive");
					request.Headers.Add("User-Agent", "ThomasDemoAgent");
					request.Headers.Add("X-Microsoft-OutputFormat", "riff-24khz-16bit-mono-pcm");

					using (var response = await client.SendAsync(request).ConfigureAwait(false))
					{
						response.EnsureSuccessStatusCode();
						using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
						{
							// Play audio with NAudio
							PlayAudioAsync(stream);
						}
					}
				}
			}
		}
		#endregion


		#region PlayAudio with NAudio
		private void PlayAudioAsync(Stream stream)
		{
			using (var audio = new WaveFileReader(stream))
			{
				using (var outputDevice = new WaveOutEvent())
				{
					outputDevice.Init(audio);
					outputDevice.Play();
					while (outputDevice.PlaybackState == PlaybackState.Playing)
					{
						Thread.Sleep(100);
					}
				}
			}
		}
		#endregion

	}
}
