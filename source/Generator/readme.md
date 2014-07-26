# Generator

This is the first version of a tool I had written which I thought might be useful. **THE PROGRAM IS OBSOLETE** by way of the introduction of a newer version: GeneratorTool.  Neither of these projects are complete, however functional and buggy in their way.  This `generator.csproj` space will be maintained until the newer GeneratorTool application fulfills the features presented here.

- 20140726 --- Checked to see if this project compiled correctly to find that it in fact did not.  This had to do with the StrongNameKey and `InternalsVisibleTo()` project/property not qualifying and allowing the internals through.
    - Added project variables to the solution [here](https://github.com/tfwio/cor3-gen/blob/master/source/Generator/Generator.csproj#L51-L53).  Note that these projects are loaded within the VisualStudio solution, aside that they are referenced separately within the csproj file here.  This semantic is used in a number of projects --- I hope to resolve an alternative, command-script or application for resolving such issues in the future.
    - Now that the project compiles, there are a few things that need to be looked at and worked into the new application.
- 20131225 --- I don't quite remember why I had added the following MSDN topics, except perhaps that I would like to see proper integration of settings.
    - [Managing app data (XAML)](http://msdn.microsoft.com/en-us/library/windows/apps/hh465099.aspx)
    - [App data (Windows Runtime apps)](http://msdn.microsoft.com/en-us/library/windows/apps/jj553522.aspx)
