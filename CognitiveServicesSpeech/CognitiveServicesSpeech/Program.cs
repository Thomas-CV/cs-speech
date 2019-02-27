using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CognitiveServicesSpeech.CognitiveServices;
using Microsoft.Extensions.Configuration;

namespace CognitiveServicesSpeech
{
	class Program
	{
		#region Fields
		private static IConfigurationRoot Configuration;
		#endregion

		#region Main
		static async Task Main(string[] args)
		{
			// Initialize configuration
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
			Configuration = builder.Build();

			// Initialize speech
			var speech = new Speech(Configuration["SpeechSubscriptionKey"], Configuration["SpeechServiceRegion"], Configuration["AuthenticationApi"], Configuration["SpeechServiceApi"]);

			// Get access token
			await speech.AuthenticateAsync();

			// Let's communicate
			await Conversation(speech, "Jessica", MessageVoice.English_US_Jessa, MessageHelper.CreateMessagesInEnglish(), MessageHelper.GetEnglishKeywords());
//			await Conversation(speech, "Marko", MessageVoice.Croatian_Matej, MessageHelper.CreateMessagesInCroatian(), MessageHelper.GetCroatianKeywords());
		}
		#endregion

		#region Conversation
		private static async Task Conversation(Speech speech, string name, string voice, string[] messages, List<MessageKeyword> keywords)
		{
			var sentence = string.Format(messages[(int)MessageIndex.Welcome], name);

			while (true)
			{
				// Say sentence
				DisplayText(sentence, ConsoleColor.Yellow);
				await speech.SpeakAsync(sentence, voice);

				// Get answer
				DisplayText("[speak now]", ConsoleColor.DarkGreen);
				var text = await speech.RecognizeSpeechAsync();
				DisplayText(text, ConsoleColor.White);

				// Calculate response
				sentence = CalculateAppropriateAnswer(text, keywords, messages);

				// Exit on end
				if (sentence == null) break;
			}
			// Say bye
			sentence = messages[(int)MessageIndex.Bye];
			DisplayText(sentence, ConsoleColor.Yellow);
			await speech.SpeakAsync(sentence, voice);
		}

		private static void DisplayText(string text, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(text);
		}
		#endregion

		#region Calculate appropriate answer
		private static string CalculateAppropriateAnswer(string text, List<MessageKeyword> keywords, string[] messages)
		{
			foreach (var keyword in keywords)
			{
				if (text.Contains(keyword.Keyword, StringComparison.OrdinalIgnoreCase) == true)
				{
					if (keyword.Index == MessageIndex.Bye) return null;
					if (keyword.Index != MessageIndex.Hello) return messages[(int)keyword.Index];
					else
					{
						var name = text.Replace(".", "");
						int index = name.LastIndexOf(' ');
						name = name.Substring(index + 1);
						return string.Format(messages[(int)keyword.Index], name);
					}
				}
			}
			return messages[(int)MessageIndex.Understand];
		}
		#endregion
	}
}
