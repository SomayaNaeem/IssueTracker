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
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			return GenerateRandomString(chars, 4);
		}
		public static string GenerateIssueId(string Projectkey)
		{
			const string numbers = "0123456789";
			return string.Concat(Projectkey,"-",1, GenerateRandomString(numbers, 2));
		}
		public static string GenerateRandomString(string pattern,int size)
		{
			Random random = new Random((int)DateTime.Now.Ticks);
			char[] chars_arr = new char[size];
			for (int i = 0; i < size; i++)
			{
				chars_arr[i] = pattern[random.Next(pattern.Length)];
			}
			return new string(chars_arr);
		}
	}
}
