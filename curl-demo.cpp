#include <iostream>
#include <string>
#include <curl/curl.h>

// Callback function to handle the data received from the server
static size_t WriteCallback(void* contents, size_t size, size_t nmemb, void* userp)
{
    ((std::string*)userp)->append((char*)contents, size * nmemb);
    return size * nmemb;
}

int main()
{
    // Initialize libcurl
    CURL* curl;
    CURLcode res;
    std::string readBuffer;

    curl_global_init(CURL_GLOBAL_DEFAULT);
    curl = curl_easy_init();
    if(curl) {
        // Set the URL for the HTTP GET request
        curl_easy_setopt(curl, CURLOPT_URL, "http://www.example.com");

        // Set the callback function to handle the data received from the server
        curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, WriteCallback);

        // Set the pointer to pass to the callback function
        curl_easy_setopt(curl, CURLOPT_WRITEDATA, &readBuffer);

        // Perform the HTTP GET request
        res = curl_easy_perform(curl);

        // Check for errors
        if(res != CURLE_OK)
            fprintf(stderr, "curl_easy_perform() failed: %s\n", curl_easy_strerror(res));

        // Cleanup
        curl_easy_cleanup(curl);

        // Output the response
        std::cout << "Response: " << readBuffer << std::endl;
    }

    curl_global_cleanup();
    return 0;
}
