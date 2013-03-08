using AvenidaSoftware.Extensions;

namespace AvenidaSoftware.HypermediaTools.Extensions {

	// This extensions exist for internal usage only
	static class internal_string_extensions {
		  public static string wordify_field(this string str, string spacer = " "){
            return str.wordify(spacer).Replace("[", spacer).Replace("]", "");
        } 
	}

}