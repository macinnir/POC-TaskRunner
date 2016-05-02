using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HooksPOC;
using System.Reflection;

namespace HooksPOCTest
{
    [TestClass]
    public class ModuleManagerTest
    {
        //[TestInitialize] - NUnit's Setup
        //[TestCleanup] - NUnit's TearDown 
        //[ClassInitialize]
        //[ClassCleanup]


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetInstanceThrowsExceptionBadClassName()
        {
            var moduleManager = new ModuleManager(); 
            moduleManager.GetInstance("foo"); 
        }

        [TestMethod]
        public void GetInstanceInstantiatesClass()
        {
            var moduleManager = new ModuleManager(); 
            var moduleA = moduleManager.GetInstance("ModuleA");
            Assert.AreEqual(moduleA.GetType().Name, "ModuleA"); 
        }
        [TestMethod]
        public void RunMethodOnDifferentAssembly()
        {
            var moduleManager = new ModuleManager();
            var assembly = Assembly.GetExecutingAssembly();
            moduleManager.SetAssembly(ref assembly);
            var testClass = moduleManager.GetInstance("TestClass");
            Assert.AreEqual(testClass.GetType().Name, "TestClass");
            moduleManager.RunModuleMethod<TestClass>((TestClass)testClass, "DoFoo"); 
        }
    }
}
