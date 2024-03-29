# Stack Validation Program
This program was made as an example to demonstrate my skills with C# and Programming in general. This application takes in a user's input and determines if this input is a valid stack.

## How to use this Unity Based Application:
A stack, delimeted by spaces, can be written into the input field on the left, and then validated using the validate button. The result will be printed into the output. Example input: 0 1 2 3 4
A file, containing lines of integers delimeted by spaces, can be validated at once using the file browser opened by the "Open File" button on the right side and then pressing "Validate" on the right side.

Example File:
```
0 1 2 3
0 3 1 2
```

This file can also have an expected output added to the end of each line. When the line is validated it will check its result against this expected result.

Example File:
```
0 1 2 3 true
0 3 1 2 false
```

A few files containing some lines to test the validator are included in this repo within the "Test Files" folder.

A premade build of the Unity Project is available in the releases (https://github.com/jonasenglish/Stack-Validation-Portfolio-App/releases/tag/Premade-Build)

The source code containing the Validation Algorithm can be located in Assets/Scripts/StackValidator.cs

Below is an example of how to use the Application:

https://github.com/jonasenglish/Series-AI-Coding-Test/assets/70903639/c859a186-3b39-4804-b6c4-d7df05d331a6


