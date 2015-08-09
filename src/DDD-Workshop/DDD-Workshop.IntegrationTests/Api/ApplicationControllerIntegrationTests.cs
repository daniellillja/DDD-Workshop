using DDD_Workshop.Domain.Application;
using NUnit.Framework;

namespace DDD_Workshop.IntegrationTests.Api
{
    public class ApplicationControllerIntegrationTests
        : RestIntegrationTests
    {
        [Test]
        [TestCase(@"Api\JsonData\ValidApplicationWIthFirstLastName.json")]
        public void ApplicantSubmitsValidApplication(string filePath)
        {
            var response = PostJsonFromFile(filePath);

            ThenServerResponseIsSuccessful(response);
            WriteServerResponseToConsole(response);

            var responseData = DeserializeJsonToDynamicObject<ApplicationEvaluatedResponse>(GetResponseJson(response));
            Assert.That(responseData.APR, Is.EqualTo(29.99));
            Assert.That(responseData.CreditLimit, Is.EqualTo(5000));
            Assert.That(responseData.ApplicationStatus, Is.EqualTo("Offered"));
        }


        [Test]
        [TestCase(@"Api\JsonData\ValidApplicationWIthFirstLastName.json")]
        public void ApplicantSubmitsTwoValidApplications(string filePath)
        {
            var response = PostJsonFromFile(filePath);
            var response2 = PostJsonFromFile(filePath);

            ThenServerResponseIsSuccessful(response2);
            WriteServerResponseToConsole(response2);

            var responseData = DeserializeJsonToDynamicObject<ApplicationEvaluatedResponse>(GetResponseJson(response2));
            Assert.That(responseData.ApplicationStatus, Is.EqualTo("Manual Evaluation"));
        }

    }



}
