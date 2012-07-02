﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Achievements;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Achievements
{
    [TestClass]
    public class CiNinjaTest
    {
        [TestMethod]
        public void HowManyTimesHasFixedSomeoneElsesBuild_NoBuilds_Zero()
        {
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>();
            Assert.AreEqual(0, CiNinja.HowManyTimesHasFixedSomeoneElsesBuild(currentBuildDefinitionOrderedChronoligically, "currentUser"));
        }

        [TestMethod]
        public void HowManyTimesHasFixedSomeoneElsesBuild_FixedSomeoneElsesBuild_One()
        {
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "someoneElse" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser" }
            };
            Assert.AreEqual(1, CiNinja.HowManyTimesHasFixedSomeoneElsesBuild(currentBuildDefinitionOrderedChronoligically, "currentUser"));
        }

        [TestMethod]
        public void HowManyTimesHasFixedSomeoneElsesBuild_FixedOwnBuild_Zero()
        {
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "currentUser" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser" }
            };
            Assert.AreEqual(0, CiNinja.HowManyTimesHasFixedSomeoneElsesBuild(currentBuildDefinitionOrderedChronoligically, "currentUser"));
        }

        [TestMethod]
        public void HowManyTimesHasFixedSomeoneElsesBuild_SomoeneElseFixedMyBuild_Zero()
        {
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "currentUser" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "someoneElse" }
            };
            Assert.AreEqual(0, CiNinja.HowManyTimesHasFixedSomeoneElsesBuild(currentBuildDefinitionOrderedChronoligically, "currentUser"));
        }

        [TestMethod]
        public void HowManyTimesHasFixedSomeoneElsesBuild_FixedSomeoneElsesBuildThenBuildAgain_OnlyOne()
        {
            var currentBuildDefinitionOrderedChronoligically = new List<BuildStatus>
            {
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Broken, RequestedBy = "someoneElse" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser" },
                new BuildStatus { BuildStatusEnum = BuildStatusEnum.Working, RequestedBy = "currentUser" },
            };
            Assert.AreEqual(1, CiNinja.HowManyTimesHasFixedSomeoneElsesBuild(currentBuildDefinitionOrderedChronoligically, "currentUser"));
        }
    }
}
