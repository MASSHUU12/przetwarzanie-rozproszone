#include "matrix.h"
#include <cstdint>
#include <cstring>
#include <iostream>

int main(int argc, char **argv) {
  if (argc < 3) {
    std::cerr << "Usage: <path_1> <path_2>\n";
    return 1;
  }

  const std::string path_1 = argv[1];
  const std::string path_2 = argv[2];

  const Matrix m1 = matrix_read(path_1);
  const Matrix m2 = matrix_read(path_2);

  if (m1.items == nullptr || m2.items == nullptr) {
    return 1;
  }

  const Matrix m3 = matrix_multiply(&m1, &m2);

  if (m3.items == nullptr) {
    return 1;
  }

  std::cout << "rows: " << m1.rows << ' ' << "columns: " << m1.cols << '\n';

  for (uint16_t i = 0; i < m3.cols; i++) {
    for (uint16_t j = 0; j < m3.rows; j++) {
      std::cout << m3.items[i * m3.rows + j] << " ";
    }
    std::cout << '\n';
  }

  matrix_save("../out.matrix", &m3);

  return 0;
}
