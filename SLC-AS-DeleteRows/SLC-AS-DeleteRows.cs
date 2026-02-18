/*
***********************************************
*  Copyright (c), Skyline Communications NV.  *
***********************************************

Revision History:

DATE		VERSION		AUTHOR			COMMENTS

17/02/2026	1.0.0.1		JST, MOD, Skyline	Initial version
****************************************************************************
*/

namespace SLCASDeleteRows
{
	using System;
	using System.Linq;

	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Automation;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;

	/// <summary>
	/// Represents a DataMiner Automation script.
	/// </summary>
	public class Script
	{
		/// <summary>
		/// The script entry point.
		/// </summary>
		/// <param name="engine">Link with SLAutomation process.</param>
		public void Run(IEngine engine)
		{
			try
			{
				RunSafe(engine);
			}
			catch (ScriptAbortException)
			{
				// Catch normal abort exceptions (engine.ExitFail or engine.ExitSuccess)
				throw; // Comment if it should be treated as a normal exit of the script.
			}
			catch (ScriptForceAbortException)
			{
				// Catch forced abort exceptions, caused via external maintenance messages.
				throw;
			}
			catch (ScriptTimeoutException)
			{
				// Catch timeout exceptions for when a script has been running for too long.
				throw;
			}
			catch (InteractiveUserDetachedException)
			{
				// Catch a user detaching from the interactive script by closing the window.
				// Only applicable for interactive scripts, can be removed for non-interactive scripts.
				throw;
			}
			catch (Exception e)
			{
				engine.ExitFail("Run|Something went wrong: " + e);
			}
		}

		private static void RunSafe(IEngine engine)
		{
			var elementIdentifier = engine.GetScriptParam(10).Value.Trim().TrimStart('[', '"').TrimEnd('"', ']');
			if (String.IsNullOrWhiteSpace(elementIdentifier))
			{
				engine.ExitFail($"Invalid Element Identifier: '{elementIdentifier}'");
				return;
			}

			var tableIdString = engine.GetScriptParam(11).Value.Trim().TrimStart('[', '"').TrimEnd('"', ']');
			if (String.IsNullOrWhiteSpace(tableIdString) || !Int32.TryParse(tableIdString, out int tableId))
			{
				engine.ExitFail($"Invalid Table ID: '{tableIdString}'");
				return;
			}

			string rowKeysString = engine.GetScriptParam(12).Value.Trim().TrimStart('[', '"').TrimEnd('"', ']');
			var rowKeys = rowKeysString.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries).Select(key => key.Trim(' ', '"')).ToList();

			if (rowKeys.Count == 0)
			{
				engine.ExitFail($"Invalid Row Key(s): '{rowKeysString}'. Expected format: 'key1,key2'.");
				return;
			}

			IDms dms = engine.GetDms();

			IDmsElement element;
			if (elementIdentifier.Contains("/"))
			{
				// If the identifier contains a '/', we assume it's in the format "DataMinerID/ElementID".
				// If incorrect, the DmsElementId constructor will throw an exception.
				element = dms.GetElement(new DmsElementId(elementIdentifier));
			}
			else
			{
				// If the identifier does not contain a '/', we assume it's an element name and try to get the element directly.
				element = dms.GetElement(elementIdentifier);
			}

			if (!element.IsStartupComplete())
			{
				engine.GenerateInformation($"Element '{element.Name}' not started.");
				return;
			}

			element.GetTable(tableId).DeleteRows(rowKeys);
		}
	}
}
