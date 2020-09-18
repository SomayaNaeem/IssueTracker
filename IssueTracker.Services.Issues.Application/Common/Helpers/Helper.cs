using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Common.Helpers
{
	public static class Helper
	{
		public static string GenerateRandomString()
		{
			Random random = new Random((int)DateTime.Now.Ticks);
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			char[] chars_arr = new char[4];
			for (int i = 0; i < 4; i++)
			{
				chars_arr[i] = chars[random.Next(chars.Length)];
			}
			return new string(chars_arr);
		}
	}
}
