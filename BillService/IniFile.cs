﻿using System;
using System.Text;






	/// <summary>


		#region 声明读写INI文件的API函数 
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

		[DllImport("kernel32")]

		[DllImport("kernel32")]













		#endregion
		/// <summary>
		{













		/// <summary>
		{
			StringBuilder temp = new StringBuilder(255);

			int i = GetPrivateProfileString(section, key, "", temp, 255, this.Path);
			return temp.ToString();













		/// <summary>