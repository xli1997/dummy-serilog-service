import re, os

def foo(required, *args, **kwargs):
    print(required)
    if args :
        print(args)
    if kwargs:
        print(kwargs)

foo("I am here", 1,2,3,4,5,6, we=123, you=345, )

        