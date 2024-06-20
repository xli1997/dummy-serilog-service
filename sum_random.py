import random

def max_subarray(numbers):
    """Find the largest sum of any contiguous subarray."""
    best_sum = - 1000
    current_sum = 0
    for x in numbers:
        current_sum = max(x, current_sum + x)
        best_sum = max(best_sum, current_sum)
        print(str(x)+"\t"+str(current_sum)+"\t"+str(best_sum)+"\n")
    return best_sum

random.seed(4)
numbers = []
for i in range(10):
    numbers.append(random.randint(-9,9))
print(numbers)
sum = max_subarray(numbers)
print(sum)