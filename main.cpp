#include "read_matrix.h"
#include <cstddef>
#include <iostream>

int main(int argc, char **argv) {
  if (argc < 3) {
    std::cerr << "Usage: <path_1> <path_2>\n";
    return 1;
  }

  std::string path_1 = argv[1];
  std::string path_2 = argv[2];

  Matrix m1 = read_matrix(path_1);
  // Matrix m2 = read_matrix(path_2);

  std::cout << "x: " << m1.x << ' ' << "y: " << m1.y << '\n';

  for (int32_t i = 0; i < m1.y; i++) {
    for (int32_t j = 0; j < m1.x; j++) {
      std::cout << m1.items[i * m1.x + j] << " ";
    }
    std::cout << std::endl;
  }

  return 0;
}
