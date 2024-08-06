#include <iostream>

class ExampleClass {
public:
    // Define the enum
    enum Status {
        primary,
        secondary
    };

    // Constructor that sets the variable to primary
    ExampleClass() : status(primary) {}

    // Function to get the current status
    Status getStatus() const {
        return status;
    }

    // Function to set the status
    void setStatus(Status newStatus) {
        status = newStatus;
    }

private:
    Status status;
};

int main() {
    // Create an instance of ExampleClass
    ExampleClass example;

    // Output the initial status
    if (example.getStatus() == ExampleClass::primary) {
        std::cout << "Status is primary." << std::endl;
    } else {
        std::cout << "Status is secondary." << std::endl;
    }

    // Set status to secondary and output the status
    example.setStatus(ExampleClass::secondary);
    if (example.getStatus() == ExampleClass::primary) {
        std::cout << "Status is primary." << std::endl;
    } else {
        std::cout << "Status is secondary." << std::endl;
    }

    return 0;
}
