using System;
using System.Text.RegularExpressions;

namespace AvenidaSoftware.HypermediaTools.Extensions {

	// This extensions exist for internal usage only
	static class internal_string_extensions {

		public static string wordify( this string s, string spacer ) {
			var wordify_regex = new Regex( "(?<=[a-z])(?<x>[A-Z])|(?<=.)(?<x>[A-Z])(?=[a-z])" );
			return string.IsNullOrEmpty(s) ? String.Empty : wordify_regex.Replace( s.Replace( " ", spacer ), " ${x}" );
		}

		public static string wordify_field( this string str, string spacer = " " ) {
			return str.wordify( spacer ).Replace( "[", spacer ).Replace( "]", "" );
		}
	}

}