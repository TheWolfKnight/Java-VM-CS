
# Java-VM-CS

## Description

This is an attempt to make a compleate __Java Virtual Machine__[^1] in C#,
to try and create a more complex program in the language.

The target is a feature complete JVM, that can do all that the official JVMS can do.

## Features

- [X] Compile java class files
- [X] Include and complie dependencies
- [ ] Type validation/Syntax validation
- [ ] execution of the program

## Useage

Complie the program like so:

__Shell__:

```Shell
dotnet.exe build --configuration Release {path/to/.csproj}
```

__Windows__:

```Batch
dotnet build --configuration Release {path/to/.csproj}
```

Run the program like so (Not Implimented):

__Shell__:

```Shell
./{path/to/executable} {path/to/main/.class}
```

__Windows__:

```Batch
{path/to/executable} {path/to/main/.class}
```

## Flags (Currently not avaliable)

- \-o/\-\-output:

puts the output[^2] into the provided file path

- \-nv/\-\-no-validation

removes the validation step, and just runs the program. Will still fail on type error.

[^1]: [JVM](https://en.wikipedia.org/wiki/Java_virtual_machine)
[^2]: [System](https://docs.oracle.com/javase/8/docs/api/java/lang/System.html)
