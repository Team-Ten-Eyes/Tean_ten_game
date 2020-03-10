using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using NUnit.Framework;

using Xamarin.Forms.Mocks;

using Game.ViewModels;
using Game.Models;

namespace UnitTests.ViewModels
{
    class BattleViewModelTests
    {
        BattleEngineViewModel ViewModel;

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            ViewModel = BattleEngineViewModel.Instance;
        }

        //unit test to see if the attibute enum changes to on
        [Test]
        public void BattleEngineViewModel_Healing_round_is_on_Should_Pass()
        {

            //Arrange
            ViewModel.Engine.RoundHealing = RoundHealingEnum.Healing_off;

            int is_on = 2;


            //Act
            ViewModel.SetRoundHealing(is_on);

            //Reset

            //Assert
            Assert.AreEqual(ViewModel.Engine.RoundHealing, RoundHealingEnum.Healing_on);

        }

        //unit test to see if the attibute enum changes to on
        [Test]
        public void BattleEngineViewModel_Healing_round_is_off_Should_Pass()
        {

            //Arrange
            ViewModel.Engine.RoundHealing = RoundHealingEnum.Healing_on;

            int is_off = 1;


            //Act
            ViewModel.SetRoundHealing(is_off);

            //Reset

            //Assert
            Assert.AreEqual(ViewModel.Engine.RoundHealing, RoundHealingEnum.Healing_off);

        }
    }
}
