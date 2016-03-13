workspace "demolisher"
	configurations { "Debug", "Release" }
	targetdir "bin/%{cfg.buildcfg}"
	
	filter "configurations:Debug"
		defines { "DEBUG" }
		flags { "Symbols" }
	
	filter "configurations:Release"
		defines { "RELEASE" }
		optimize "On"
	
	project "demolisher"
		kind "WindowedApp"
		language "C#"
		namespace "arookas"
		location "demolisher"
		
		links {
			"arookas",
			"OpenTK",
			"OpenTK.GLControl",
			"System",
			"System.Drawing",
			"System.Windows.Forms",
		}
		
		files {
			"demolisher/**.cs",
			"demolisher/**.resx",
		}
		
		excludes {
			"demolisher/bin/**",
			"demolisher/obj/**",
		}
