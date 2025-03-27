cmake_minimum_required(VERSION 3.30)
project(program)

set(CMAKE_CXX_COMPILER "clang++")
set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -Wall -Wextra -std=c++11 -fopenmp")

set(SOURCES
    main.cpp
    matrix.cpp
)

add_executable(${PROJECT_NAME} ${SOURCES})
