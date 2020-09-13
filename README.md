# Perley Develop

This is an attempt to make a usable IDE, not award winning, just usable. 

I am working on getting .net core running first and then maybe implementing another language later. It is based on Gtk and is heavily inspired by Monodevelop. 

Visual studio and code as well, but Monodevelop for some reason, has always been a favorite.  

The IDE is going to use a plugin system for extensability and in some cases simplicity.

# Planned Compilers
I will be using 2 different compilers, the first is the dotnet Cli, which is already implemented and the other I am still working on, on the side. However the Cli version is built directly into the editor at this point. It will be removed, but when I do not know.

    --Compiler 1: Console based, runs console commands in background using dotnet CLI.
    --Compiler 2: C# .net core compiler implementation. -- under construction.

Both compilers will be available via plugins and can be turned on or off in the editor. (todo, not currently added)

# Debugging
I plan to explore a few other options as well, but Samsung has an open source .Net Core debugger I should be able to utilize. Apparently vsdbg is not available outside MS products?

I will be implementing debugger functionality via plugins as well, so users can use the base debugger or a custom one if they so choose. Project types will be supplied later on. Way to much to think about before then.

# Other Stuff
There are some other features I want to implement such as:
    --Git Support (Later)
    --Docker Support (Later)
    --Syntax Highlighting (Duh)
    --Code Completion (Later)
    --Nuget Browser (Later, currently using dotnet cli for nuget.)

# Notes: 
The code is messy, but that will change. There are going to be several updates to this repo over time. Some code will get better, some will get worse. This is not about best practices. 

 -- There are libraries the project relies on that I will not be releasing source code for. 
 -- Plugin development details and apis will be available as soon as I have the time. 