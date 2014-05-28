using System;
using System.ComponentModel;

namespace Generator.Controls
{
	public class BgHelper<TResult> : BackgroundWorker where TResult:class
	{
		public TResult ResultObject { get;set; }
		public Action<TResult> ActionComplete { get;set; }
		
		protected override void OnDoWork(DoWorkEventArgs e)
		{
			base.OnDoWork(e);
		}
		
		protected override void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
		{
			base.OnRunWorkerCompleted(e);
			//if (ResultObject==null) ResultObject=null;
			if (ActionComplete!=null) ActionComplete.Invoke(ResultObject);
		}
	}
}
