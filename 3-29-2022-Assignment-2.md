# History of Linux
## What is Linux?
- A free open source operating system created by Linus Torvalds with assistance from developers worldwide
- Linus Torvalds developed the Linux Kernel in 1991
- Created due to a perceived lacking of features with Minix ( a small version of Unix meant for academic purposes)
- It is a kernel - provides access to computer hardware and control access to resources like: 
  - Files and data
  - Running programs
  - Loading programs into memory
  - Networks
  - Security and Firewalls
- Shell:
  - An interactive user interface between the user and the operating system
  - Gives access to services of the kernel
  - Executes commands
  - Can be used as a programming language or a command interpreter
  - Reads commands from input lines or files
## Brief [Shell History](https://developer.ibm.com/tutorials/l-linux-shells/)
- Ken Thompson of Bell Labs developed the first Shell in 1971 for Unix called V6
- Multiple different shells were made from the beginning one, branching off for different purposes/specifications
- In 1989 the BASH shell was created
- Bash has elements from C and Korn Shells
- Bash is backwards compatible with the Bourne Shell
- Bash is still being developed today

![image.txt](https://developer.ibm.com/developer/default/tutorials/l-linux-shells/images/figure1.gif)

# Getting Started with Shell Programming
- The Bash Shell takes commands in from the user or a file and uses said commands to execute certain tasks.
- There are three parts to a command
  - The command itself
  - Options to the command that start with a - or – 
  - The Argument for the Command
- Bash Shell understands certain commands
  - Aliases
  - Keywords
  - Functions (user defined and built in, can be determined by the type command)
  - File paths
  - For full list of [commands](https://bash.cyberciti.biz/guide/Shell_commands#Executing_commands_using_the_Bash_Shell)
- Users can login with the shell environment
- There are a variety of other shells besides BASH
- #! Can be used to execute an interpreter
- '#' By itself can be used to comment out code or to create comments
  - Comments do not affect execution speed
  - <<COMMENT1 … COMMENT1 can be used to comment out multiple lines
- chmod can be used to limit who can run shell scripts where. +x means anyone can
- To execute a script do
  - chmod +x script.sh
  - ./script.sh
- Or
  - bash script.sh
  - . script.sh
- bash -x script can also be used for debugging

