/* oio : 12/08/2013 14:03 */
using System;
using System.Cor3.Data.Engine;
using System.Data.SQLite;

namespace mp4nfo.Library
{
	/// <summary>this file originated within my little mp4util (wpf)</summary>
	public class SQLiteAbstract : IDisposable
	{
		#region Properties
		public string FilePath { get;set; }
		public SQLiteDb Vat { get;set; }
		
		public SQLiteConnection Connection { get;set; }
		public SQLiteDataAdapter Adapter { get;set; }
		public SQLiteCommand Command { get;set; }
		#endregion
		#region .Ctor + Methods
		/// <summary>
		/// Used to create a data-connection.
		/// Don't forget to dispose.
		/// </summary>
		/// <param name="dataFilePath"></param>
		public SQLiteAbstract(string dataFilePath)
		{
			FilePath = dataFilePath;
			if (!System.IO.File.Exists(dataFilePath)) return;
			SQLiteDb db = new SQLiteDb(dataFilePath);
			SQLiteConnection c = db.Connection;
			SQLiteDataAdapter a = db.Adapter;
		}
		public void Dispose()
		{
			if (Adapter != null) {Adapter.Dispose(); Adapter = null; }
			if (Connection != null) {Connection.Dispose(); Connection = null; }
			if (Vat != null) { Vat.Dispose(); Vat = null; }
		}
		#endregion
	}
}
