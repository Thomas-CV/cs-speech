using System.Collections.Generic;

namespace CognitiveServicesSpeech
{
	public enum MessageIndex
	{
		Welcome,
		Hello,
		Understand,
		Help,
		Vacation,
		Drinks,
		Sure,
		Thanks,
		Bye
	}

	public static class MessageVoice
	{
		public const string English_US_Jessa = "Microsoft Server Speech Text to Speech Voice (en-US, Jessa24kRUS)";
		public const string English_US_Guy = "Microsoft Server Speech Text to Speech Voice (en-US, Guy24kRUS)";
		public const string Croatian_Matej = "Microsoft Server Speech Text to Speech Voice (hr-HR, Matej)";
	}

	public static class MessageHelper
	{
		public static string[] CreateMessagesInEnglish()
		{
			return new string[]
			{
				"Welcome to our call centre. I'm {0}. What's your name?",
				"Hello {0}. How can I help you?",
				"Sorry but I didn't understand you. Could you please repeat that.",
				"Please let me know if I can be of any help.",
				"I would like that too. We can go together right after the meetup.",
				"That's a great choice. But this is a call centre, can I help you with some of our services?",
				"Great, I'm here for you.",
				"You are welcome.",
				"It was nice talking with you. Please send my regards to the audience in Rijeka."
			};
		}
		public static string[] CreateMessagesInCroatian()
		{
			return new string[]
			{
				"Dobrodošli u naš centar za korisnike. Moje ime je {0}. Kako se vi zovete?",
				"Pozdrav {0}. Kako vam mogu pomoći?",
				"Oprostite ali nisam vas razumio. Možete li molim vas ponoviti.",
				"Na raspolaganju sam vam ukoliko trebate bilo kakvu dodatnu pomoć.",
				"I ja bi to volio. Možemo zajedno odmah nakon predavanja.",
				"Odličan odabir. Na žalost mogu vam pomoći jedino u svezi naših usluga.",
				"Odlično, javite ako što trebate.",
				"Hvala vama.",
				"Hvala vam na pozivu. Molim vas da prenesete moje pozdrave svima u Riječkoj publici."
			};
		}

		public static List<MessageKeyword> GetEnglishKeywords()
		{
			return new List<MessageKeyword>()
			{
				new MessageKeyword() { Keyword = "name", Index = MessageIndex.Hello },
				new MessageKeyword() { Keyword = "coffee", Index = MessageIndex.Drinks },
				new MessageKeyword() { Keyword = "tea", Index = MessageIndex.Drinks },
				new MessageKeyword() { Keyword = "nothing", Index = MessageIndex.Help },
				new MessageKeyword() { Keyword = "not", Index = MessageIndex.Help },
				new MessageKeyword() { Keyword = "vacation", Index = MessageIndex.Vacation },
				new MessageKeyword() { Keyword = "travel", Index = MessageIndex.Vacation },
				new MessageKeyword() { Keyword = "sure", Index = MessageIndex.Sure },
				new MessageKeyword() { Keyword = "will", Index = MessageIndex.Sure },
				new MessageKeyword() { Keyword = "thank", Index = MessageIndex.Thanks },
				new MessageKeyword() { Keyword = "bye", Index = MessageIndex.Bye }
			};
		}

		public static List<MessageKeyword> GetCroatianKeywords()
		{
			return new List<MessageKeyword>()
			{
				new MessageKeyword() { Keyword = "ime", Index = MessageIndex.Hello },
				new MessageKeyword() { Keyword = "kavu", Index = MessageIndex.Drinks },
				new MessageKeyword() { Keyword = "čaj", Index = MessageIndex.Drinks },
				new MessageKeyword() { Keyword = "ništa", Index = MessageIndex.Help },
				new MessageKeyword() { Keyword = "ne", Index = MessageIndex.Help },
				new MessageKeyword() { Keyword = "hvala", Index = MessageIndex.Help },
				new MessageKeyword() { Keyword = "odmor", Index = MessageIndex.Vacation },
				new MessageKeyword() { Keyword = "godišnji", Index = MessageIndex.Vacation },
				new MessageKeyword() { Keyword = "put", Index = MessageIndex.Vacation },
				new MessageKeyword() { Keyword = "svakako", Index = MessageIndex.Sure },
				new MessageKeyword() { Keyword = "budem", Index = MessageIndex.Sure },
				new MessageKeyword() { Keyword = "redu", Index = MessageIndex.Sure },
				new MessageKeyword() { Keyword = "hvala", Index = MessageIndex.Thanks },
				new MessageKeyword() { Keyword = "pozdrav", Index = MessageIndex.Bye },
				new MessageKeyword() { Keyword = "đenja", Index = MessageIndex.Bye },
				new MessageKeyword() { Keyword = "bye", Index = MessageIndex.Bye }
			};
		}
	}
}
