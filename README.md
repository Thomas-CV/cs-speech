# Cognitive Services Speech Sample

This sample contains an application for speech to text and text to speech using Speech API from [Microsoft Cognitive Services](https://azure.microsoft.com/en-us/services/cognitive-services/). The application is implemented in C# / .NET Core 2.1.

## Getting Started

1. Get API key for the **Speech API** from [Microsoft Cognitive Services](https://azure.microsoft.com/en-us/services/cognitive-services/).
2. Open the sample in Visual Studio 2017 or newer:
    - replace text "**replace_with_your_subscription_key**" in appsettings.json with obtained subscription key.
    - if needed verify that the ServiceEndpoint in appsettings.json is the same as obtained from Microsoft's web.
3. Build and run the sample application.
    - be sure to build for C# 7.1 or higher (Project - Properties - Build - Advanced - Language version - C# 7.1) which supports async Main method.

## Usage

1. Build and Start.
2. Try saying the following sentences:
    - My name is "YOUR_NAME"
    - I would like coffee please
    - I realy need the vacation?
    - Bye, bye.