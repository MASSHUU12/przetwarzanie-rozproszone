#include "matrix.h"
#include <cstdint>
#include <cstring>
#include <iostream>
#include <unistd.h>

int main(int argc, char **argv) {
  int32_t opt;
  std::string in1 = "../m1.matrix", in2 = "../m2.matrix", out = "../out.matrix";

  while ((opt = getopt(argc, argv, "a:b:o:")) != -1) {
    switch (opt) {
      case 'a':
        in1 = optarg;
      case 'b':
        in2 = optarg;
      case 'o':
        out = optarg;
      default:
        std::cerr << "Usage: [ -a in_path ] [ -b in_path ] [ -o out_path ] <\n";
        return 1;
    }
  }

  const Matrix m1 = matrix_read(in1);
  const Matrix m2 = matrix_read(in2);

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
