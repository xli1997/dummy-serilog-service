#include <iostream>
#include <rapidjson/document.h>
#include <rapidjson/writer.h>
#include <rapidjson/stringbuffer.h>

int main() {
    // Example JSON string
    const char* json = R"([
        {"name": "John", "age": 30, "city": "New York"},
        {"name": "Jane", "age": 25, "city": "Chicago"}
    ])";

    // Parse the JSON string
    rapidjson::Document document;
    if (document.Parse(json).HasParseError()) {
        std::cerr << "Error parsing JSON" << std::endl;
        return 1;
    }

    // Check if the root is an array
    if (!document.IsArray()) {
        std::cerr << "JSON is not an array" << std::endl;
        return 1;
    }

    // Iterate over the array
    for (const auto& item : document.GetArray()) {
        if (item.IsObject()) {
            // Access the object properties
            if (item.HasMember("name") && item["name"].IsString()) {
                std::cout << "Name: " << item["name"].GetString() << std::endl;
            }
            if (item.HasMember("age") && item["age"].IsInt()) {
                std::cout << "Age: " << item["age"].GetInt() << std::endl;
            }
            if (item.HasMember("city") && item["city"].IsString()) {
                std::cout << "City: " << item["city"].GetString() << std::endl;
            }
        }
    }

    return 0;
}
