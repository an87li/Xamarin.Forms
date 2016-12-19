using System;
using System.IO;
using System.Collections.Generic;
using Xamarin.UITest;
using Xamarin.UITest.Desktop;
using Xamarin.UITest.Queries;
using System.Linq;
using System.Diagnostics;

namespace Xamarin.Forms.Core.macOS.UITests
{
	public static class MacOSAppExtensions
	{
		public static UITest.Queries.AppResult ToUITestResult(this UITest.Desktop.AppResult result)
		{
			return new UITest.Queries.AppResult
			{
				Id = result.Id ?? result.TestId,
				Label = result.Label,
				Text = result.Text ?? result.Value,
				Enabled = result.Enabled,
				Class = result.Class,
				Rect = new UITest.Queries.AppRect
				{
					X = result.Rect.X,
					Y = result.Rect.Y,
					Width = result.Rect.Width,
					Height = result.Rect.Height,
					CenterX = result.Rect.CenterX,
					CenterY = result.Rect.CenterY
				}
			};
		}
	}


	public class MacOSApp : Xamarin.UITest.IApp
	{
		CocoaApp _cocoaApp;
		public MacOSApp(CocoaApp app)
		{
			_cocoaApp = app;
		}
		public IDevice Device
		{
			get
			{
				return null;
			}
		}

		public AppPrintHelper Print
		{
			get
			{
				return null;
			}
		}

		public ITestServer TestServer
		{
			get
			{
				return null;
			}
		}

		public void Back()
		{
			Tap("NSBackButton");
		}

		public void ClearText()
		{

		}

		public void ClearText(string marked)
		{

		}

		public void ClearText(Func<AppQuery, AppWebQuery> query)
		{

		}

		public void ClearText(Func<AppQuery, AppQuery> query)
		{

		}

		public void DismissKeyboard()
		{

		}

		public void DoubleTap(string marked)
		{

		}

		public void DoubleTap(Func<AppQuery, AppQuery> query)
		{

		}

		public void DoubleTapCoordinates(float x, float y)
		{

		}

		public void DragAndDrop(string from, string to)
		{

		}

		public void DragAndDrop(Func<AppQuery, AppQuery> from, Func<AppQuery, AppQuery> to)
		{

		}

		public void DragCoordinates(float fromX, float fromY, float toX, float toY)
		{

		}

		public void EnterText(string text)
		{
			var textField = _cocoaApp.QueryByType("Textfield").First();
			_cocoaApp.Click(textField.Rect.CenterX, textField.Rect.CenterY);
			_cocoaApp.EnterText(text);
		}

		public void EnterText(Func<AppQuery, AppWebQuery> query, string text)
		{

		}

		public void EnterText(string marked, string text)
		{
			var textField = _cocoaApp.QueryById(marked).FirstOrDefault((arg) => arg.Class.Contains("SearchField") || arg.Class.Contains("TextField"));
			_cocoaApp.Click(textField.Rect.CenterX, textField.Rect.CenterY);
			_cocoaApp.EnterText(text);
		}

		public void EnterText(Func<AppQuery, AppQuery> query, string text)
		{
			var queryStr = query(new AppQuery(QueryPlatform.iOS)).ToString();

			var isMarked = System.Text.RegularExpressions.Regex.IsMatch(queryStr, @"\bmarked\b");
			if (isMarked)
			{
				var markedWord = System.Text.RegularExpressions.Regex.Split(queryStr, @"\bmarked\b:'");
				var isAll = markedWord[0].Trim() == "*";
				var marked = markedWord[1].Replace("'", "");
				EnterText(marked, text);
			}
		}

		public UITest.Queries.AppResult[] Flash(string marked)
		{
			var resulr = new List<Xamarin.UITest.Queries.AppResult>();
			return resulr.ToArray();
		}

		public UITest.Queries.AppResult[] Flash(Func<AppQuery, AppQuery> query = null)
		{
			var resulr = new List<Xamarin.UITest.Queries.AppResult>();
			return resulr.ToArray();
		}

		public object Invoke(string methodName, object[] arguments)
		{
			return null;
		}

		public object Invoke(string methodName, object argument = null)
		{
			return null;
		}

		public void PinchToZoomIn(string marked, TimeSpan? duration = default(TimeSpan?))
		{

		}

		public void PinchToZoomIn(Func<AppQuery, AppQuery> query, TimeSpan? duration = default(TimeSpan?))
		{

		}

		public void PinchToZoomInCoordinates(float x, float y, TimeSpan? duration)
		{

		}

		public void PinchToZoomOut(string marked, TimeSpan? duration = default(TimeSpan?))
		{

		}

		public void PinchToZoomOut(Func<AppQuery, AppQuery> query, TimeSpan? duration = default(TimeSpan?))
		{

		}

		public void PinchToZoomOutCoordinates(float x, float y, TimeSpan? duration)
		{

		}

		public void PressEnter()
		{
			_cocoaApp.SendKey(13, KeyModifier.None);
		}

		public void PressVolumeDown()
		{

		}

		public void PressVolumeUp()
		{

		}

		public AppWebResult[] Query(Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppWebQuery> query)
		{
			var resulr = new List<Xamarin.UITest.Queries.AppWebResult>();
			return resulr.ToArray();
		}

		public string[] Query(Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.InvokeJSAppQuery> query)
		{
			return new List<string>().ToArray();
		}

		public UITest.Queries.AppResult[] Query(string marked)
		{
			var results = new List<Xamarin.UITest.Queries.AppResult>();
			var allResults = _cocoaApp.Query();
			var allResultsById = _cocoaApp.QueryById(marked);
			foreach (var result in allResultsById)
				results.Add(result.ToUITestResult());
			return results.ToArray();
		}

		public UITest.Queries.AppResult[] Query(Func<UITest.Queries.AppQuery, UITest.Queries.AppQuery> query = null)
		{
			var queryStr = query(new AppQuery(QueryPlatform.iOS)).ToString();
			var results = new List<Xamarin.UITest.Queries.AppResult>();
			var isMarked = System.Text.RegularExpressions.Regex.IsMatch(queryStr, @"\bmarked\b");
			if (isMarked)
			{
				var markedWords = System.Text.RegularExpressions.Regex.Split(queryStr, @"\bmarked\b:'");
				var isAll = markedWords[0].Trim() == "*";
				var markedWord = markedWords[1].Replace("'", "");
				return Query(markedWord);
			}
			else if (queryStr.Contains("* index:0"))
			{
				var allREsults = _cocoaApp.Query();
				var result = allREsults[0].Children[0];
				results.Add(result.ToUITestResult());
			}
			else if (queryStr.Contains("* index:7"))
			{
				var allREsults = _cocoaApp.Query();
				var result = allREsults[0].Children[0].Children[0].Children[1];
				results.Add(result.ToUITestResult());
			}

			return results.ToArray();
		}

		public T[] Query<T>(Func<UITest.Queries.AppQuery, UITest.Queries.AppTypedSelector<T>> query)
		{

			var results = new List<T>();
			var queryStr = query(new AppQuery(QueryPlatform.iOS)).ToString();
			var isMarked = System.Text.RegularExpressions.Regex.IsMatch(queryStr, @"\bmarked\b");
			if (isMarked)
			{
				var markedWords = System.Text.RegularExpressions.Regex.Split(queryStr, @"\bmarked\b:'");
				var isAll = markedWords[0].Trim() == "*";
				var markedWord = markedWords[1].Replace("'", "");
				var ss = Query(markedWord);

			}
			else if (queryStr.Contains("* index:0"))
			{

				var allREsults = _cocoaApp.Query();
				var result = allREsults[0].Children[0];
				//	results.Add(result.ToUITestResult());
			}
			else if (queryStr.Contains("* index:7"))
			{
				var allREsults = _cocoaApp.Query();
				var result = allREsults[0].Children[0].Children[0].Children[1];
				//	results.Add(result.ToUITestResult());
			}

			return results.ToArray();
		}

		public void Repl()
		{

		}

		public FileInfo Screenshot(string title)
		{
			return null;
		}

		public void ScrollDown(string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void ScrollDown(Func<AppQuery, AppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void ScrollDownTo(Func<AppQuery, AppQuery> toQuery, Func<AppQuery, AppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = default(TimeSpan?))
		{

		}

		public void ScrollDownTo(Func<AppQuery, AppWebQuery> toQuery, Func<AppQuery, AppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = default(TimeSpan?))
		{

		}

		public void ScrollDownTo(Func<AppQuery, AppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = default(TimeSpan?))
		{

		}

		public void ScrollDownTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = default(TimeSpan?))
		{

		}

		public void ScrollTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = default(TimeSpan?))
		{

		}

		public void ScrollUp(string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void ScrollUp(Func<AppQuery, AppQuery> query = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void ScrollUpTo(Func<AppQuery, AppWebQuery> toQuery, string withinMarked, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = default(TimeSpan?))
		{

		}

		public void ScrollUpTo(Func<AppQuery, AppQuery> toQuery, Func<AppQuery, AppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = default(TimeSpan?))
		{

		}

		public void ScrollUpTo(Func<AppQuery, AppWebQuery> toQuery, Func<AppQuery, AppQuery> withinQuery = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = default(TimeSpan?))
		{

		}

		public void ScrollUpTo(string toMarked, string withinMarked = null, ScrollStrategy strategy = ScrollStrategy.Auto, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true, TimeSpan? timeout = default(TimeSpan?))
		{

		}

		public void SetOrientationLandscape()
		{

		}

		public void SetOrientationPortrait()
		{

		}

		public void SetSliderValue(Func<AppQuery, AppQuery> query, double value)
		{

		}

		public void SetSliderValue(string marked, double value)
		{

		}

		public void SwipeLeft()
		{

		}

		public void SwipeLeftToRight(double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void SwipeLeftToRight(Func<AppQuery, AppQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void SwipeLeftToRight(Func<AppQuery, AppWebQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{
			throw new NotImplementedException();
		}

		public void SwipeLeftToRight(string marked, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void SwipeRight()
		{

		}

		public void SwipeRightToLeft(double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void SwipeRightToLeft(Func<AppQuery, AppWebQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{
			throw new NotImplementedException();
		}

		public void SwipeRightToLeft(Func<AppQuery, AppQuery> query, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void SwipeRightToLeft(string marked, double swipePercentage = 0.67, int swipeSpeed = 500, bool withInertia = true)
		{

		}

		public void Tap(Func<AppQuery, AppWebQuery> query)
		{

		}

		public void Tap(string marked)
		{
			var queryById = _cocoaApp.QueryById(marked).First();
			_cocoaApp.Click(queryById.Rect.CenterX, queryById.Rect.CenterY);
		}

		public void Tap(Func<AppQuery, AppQuery> query)
		{
			var queryStr = query(new AppQuery(QueryPlatform.iOS)).ToString();
			var isMarked = System.Text.RegularExpressions.Regex.IsMatch(queryStr, @"\bmarked\b");
			if (isMarked)
			{
				var markedWords = System.Text.RegularExpressions.Regex.Split(queryStr, @"\bmarked\b:'");
				var isAll = markedWords[0].Trim() == "*";
				var markedWord = markedWords[1].Replace("'", "");
				Tap(markedWord);
			}
			var isText = System.Text.RegularExpressions.Regex.IsMatch(queryStr, @"\btext\b");
			if (isText)
			{
				var markedWords = System.Text.RegularExpressions.Regex.Split(queryStr, @"\btext\b:'");
				var isAll = markedWords[0].Trim() == "*";
				var markedWord = markedWords[1].Replace("'", "");
				Tap(markedWord);
			}
		}

		public void TapCoordinates(float x, float y)
		{

		}

		public void TouchAndHold(string marked)
		{

		}

		public void TouchAndHold(Func<AppQuery, AppQuery> query)
		{

		}

		public void TouchAndHoldCoordinates(float x, float y)
		{

		}

		public void WaitFor(Func<bool> predicate, string timeoutMessage = "Timed out waiting...", TimeSpan? timeout = default(TimeSpan?), TimeSpan? retryFrequency = default(TimeSpan?), TimeSpan? postTimeout = default(TimeSpan?))
		{

		}

		public AppWebResult[] WaitForElement(Func<AppQuery, AppWebQuery> query, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = default(TimeSpan?), TimeSpan? retryFrequency = default(TimeSpan?), TimeSpan? postTimeout = default(TimeSpan?))
		{
			var resulr = new List<Xamarin.UITest.Queries.AppWebResult>();
			return resulr.ToArray();
		}

		public UITest.Queries.AppResult[] WaitForElement(string marked, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = default(TimeSpan?), TimeSpan? retryFrequency = default(TimeSpan?), TimeSpan? postTimeout = default(TimeSpan?))
		{
			var results = new List<Xamarin.UITest.Queries.AppResult>();

			var queryById = _cocoaApp.QueryById(marked);
			foreach (var res in queryById)
			{
				results.Add(res.ToUITestResult());
			}
			Stopwatch s = new Stopwatch();
			s.Start();
			bool foundElement = false;
			while (s.Elapsed < timeout && !foundElement)
			{
				var allResultsById = _cocoaApp.QueryById(marked);
				foreach (var res in queryById)
				{
					results.Add(res.ToUITestResult());
				}
				foundElement = results.Count > 0;
				System.Diagnostics.Debug.WriteLine(foundElement);
			}
			s.Stop();

			return results.ToArray();
		}

		public UITest.Queries.AppResult[] WaitForElement(Func<AppQuery, AppQuery> query, string timeoutMessage = "Timed out waiting for element...", TimeSpan? timeout = default(TimeSpan?), TimeSpan? retryFrequency = default(TimeSpan?), TimeSpan? postTimeout = default(TimeSpan?))
		{
			var queryStr = query(new AppQuery(QueryPlatform.iOS)).ToString();
			var isMarked = System.Text.RegularExpressions.Regex.IsMatch(queryStr, @"\bmarked\b");
			if (isMarked)
			{
				var markedWords = System.Text.RegularExpressions.Regex.Split(queryStr, @"\bmarked\b:'");
				var isAll = markedWords[0].Trim() == "*";
				var markedWord = markedWords[1].Replace("'", "");
				return WaitForElement(markedWord, timeoutMessage, timeout, retryFrequency, postTimeout);
			}
			return new List<Xamarin.UITest.Queries.AppResult>().ToArray();
		}

		public void WaitForNoElement(Func<AppQuery, AppWebQuery> query, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = default(TimeSpan?), TimeSpan? retryFrequency = default(TimeSpan?), TimeSpan? postTimeout = default(TimeSpan?))
		{


		}

		public void WaitForNoElement(string marked, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = default(TimeSpan?), TimeSpan? retryFrequency = default(TimeSpan?), TimeSpan? postTimeout = default(TimeSpan?))
		{
			Stopwatch s = new Stopwatch();
			s.Start();
			bool noElement = false;
			while (s.Elapsed < timeout && !noElement)
			{
				var allResultsById = _cocoaApp.QueryById(marked);
				noElement = allResultsById.Length == 0;
				System.Diagnostics.Debug.WriteLine(noElement);
			}
			s.Stop();
			if (s.Elapsed < timeout && !noElement)
				throw (new Exception(timeoutMessage));

		}

		public void WaitForNoElement(Func<AppQuery, AppQuery> query, string timeoutMessage = "Timed out waiting for no element...", TimeSpan? timeout = default(TimeSpan?), TimeSpan? retryFrequency = default(TimeSpan?), TimeSpan? postTimeout = default(TimeSpan?))
		{
			var queryStr = query(new AppQuery(QueryPlatform.iOS)).ToString();
			var isMarked = System.Text.RegularExpressions.Regex.IsMatch(queryStr, @"\bmarked\b");
			if (isMarked)
			{
				var markedWords = System.Text.RegularExpressions.Regex.Split(queryStr, @"\bmarked\b:'");
				var isAll = markedWords[0].Trim() == "*";
				var markedWord = markedWords[1].Replace("'", "");
				WaitForNoElement(markedWord, timeoutMessage, timeout, retryFrequency, postTimeout);
			}
		}


	}
}