﻿using System;
using GooseEngine.Rule;
using GooseEngine.Rule.Exceptions;
using NUnit.Framework;


namespace GooseEngine_Test.Rule
{
    [TestFixture]
    public class RuleHierarchTest
    {
        [Test]
        public void Conclude_aLowPriorityAndAHighPrioryWithCondictionaryConclusions_ConcludeBasedOnTheHighPrioritiesRules()
        {
            TransformationRule<int> lowpriority = new TransformationRule<int>();
            TransformationRule<int> highpriority = new TransformationRule<int>();
            RuleHierarch<string, int> hierarch = new RuleHierarch<string, int>();
            hierarch.AddLayer("low priority", lowpriority);
            hierarch.AddLayer("high priority", highpriority);

            lowpriority.AddPremise(i => i == 1, new Conclusion("not correct"));
            highpriority.AddPremise(i => i == 1, new Conclusion("correct"));

            string expected = "correct";
            string actual = hierarch.Conclude(1).ToString();
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void Conclude_aLowPriorityAndAHighPrioryWhereHighPriorityHaveNoOpinion_ConcludeBasedOnTheLowPrioritiesRules()
        {
            TransformationRule<int> lowpriority = new TransformationRule<int>();
            TransformationRule<int> highpriority = new TransformationRule<int>();
            RuleHierarch<string, int> hierarch = new RuleHierarch<string, int>();
            hierarch.AddLayer("low priority", lowpriority);
            hierarch.AddLayer("high priority", highpriority);

            lowpriority.AddPremise(i => i == 1, new Conclusion("not correct"));

            string expected = "not correct";
            string actual = hierarch.Conclude(1).ToString();
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void Conclude_aLowPriorityAndAHighPrioryWhereHighPriorityAndLowPriorityHaveNoOpinion_ConcludeDontCare()
        {
            TransformationRule<int> lowpriority = new TransformationRule<int>();
            TransformationRule<int> highpriority = new TransformationRule<int>();
            RuleHierarch<string, int> hierarch = new RuleHierarch<string, int>();
            hierarch.AddLayer("low priority", lowpriority);
            hierarch.AddLayer("high priority", highpriority);

            Assert.IsInstanceOf<DontCareConclusion>(hierarch.Conclude(1));

        }


    }

}