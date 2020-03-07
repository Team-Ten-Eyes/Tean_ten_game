﻿using Game.ViewModels;
using NUnit.Framework;
using System.Threading.Tasks;
using Xamarin.Forms.Mocks;

namespace UnitTests.ViewModels
{
    public class ItemIndexViewModelTests
    {
        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            // Activate the Datastore
            ScoreIndexViewModel.Instance.GetCurrentDataSource();
            ItemIndexViewModel.Instance.GetCurrentDataSource();
        }

        [Test]
        public async Task ItemIndexViewModel_Read_Invalid_ID_Bogus_Should_Fail()
        {
            // Arrange

            // Act
            var result = await ItemIndexViewModel.Instance.ReadAsync("bogus");

            // Reset

            // Assert
            Assert.IsNull(result);
        }
    }
}