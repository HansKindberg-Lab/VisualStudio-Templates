using System.Linq;
using EnvDTE;
using HansKindberg.VisualStudio.Templates.Wizards.Environment.Extensions;
using HansKindberg.VisualStudio.Templates.Wizards.IntegrationTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VSLangProj;

namespace HansKindberg.VisualStudio.Templates.Wizards.IntegrationTests.Environment.Extensions
{
	[TestClass]
	public class SolutionExtensionTest
	{
		#region Fields

		private static readonly Solution _solution = EnvironmentHelper.GetActiveSolution();

		#endregion

		#region Properties

		private static Solution Solution
		{
			get { return _solution; }
		}

		#endregion

		#region Methods

		[TestMethod]
		public void GetAllProjects_ShouldReturnACollectionIncludingTenVisualStudioProjects()
		{
			Assert.AreEqual(10, Solution.GetAllProjects().Select(project => project.Object).OfType<VSProject>().Count());
		}

		[TestMethod]
		public void GetAllProjects_ShouldReturnTwentySixProjects()
		{
			Assert.AreEqual(26, Solution.GetAllProjects().Count());
		}

		[TestMethod]
		public void PrerequisiteTest()
		{
			Assert.AreEqual(7, Solution.Projects.Count);

			var projects = Solution.Projects.Cast<Project>().ToArray();

			Assert.AreEqual(".nuget", projects.ElementAt(0).Name);
			Assert.AreEqual("Properties", projects.ElementAt(1).Name);
			Assert.AreEqual("CodeAnalysis", projects.ElementAt(2).Name);
			Assert.AreEqual("Templates", projects.ElementAt(3).Name);
			Assert.AreEqual("Template-Assemblies", projects.ElementAt(4).Name);
			Assert.AreEqual("Package", projects.ElementAt(5).Name);

			var visualStudioProjects = projects.Select(project => project.Object).OfType<VSProject>();

			Assert.AreEqual(1, visualStudioProjects.Count());
		}

		#endregion
	}
}