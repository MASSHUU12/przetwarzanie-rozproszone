cmake_minimum_required(VERSION 3.0)
project(program)

set(CMAKE_CXX_COMPILER "clang++")
set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -Wall -Wextra -std=c++11")

set(SOURCES
    main.cpp
    read_matrix.cpp
)

add_executable(${PROJECT_NAME} ${SOURCES})
