# BinaryMod
This project uses dnlib to modify data inside a binary.
I've seen this being used especially in RATs/Crypters.
The basic idea is that you create strings as placeholdes inside your "stub", compile it and instead of using codedom to recompile the code you instead use this to change the placeholders to prefered values.
Please read the code carefully.
If i missed something or if you think you can make it better let me know :)
