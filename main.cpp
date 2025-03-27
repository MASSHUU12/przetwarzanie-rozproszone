#include "matrix.h"
#include <cstdint>
#include <cstring>
#include <iostream>
#include <unistd.h>

int main(int argc, char **argv) {
  int32_t opt;
  bool verbose = false;
  std::string in1 = "../m1.matrix", in2 = "../m2.matrix", out = "";

  while ((opt = getopt(argc, argv, "a:b:o:v")) != -1) {
    switch (opt) {
    case 'a':
      in1 = optarg;
      break;
    case 'b':
      in2 = optarg;
      break;
    case 'o':
      out = optarg;
      break;
    case 'v':
      verbose = true;
      break;
    default:
      std::cerr
          << "Usage: [ -a in_path ] [ -b in_path ] [ -o out_path ] [ -v ] \n";
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

  if (verbose) {
    std::cout << "A:\n";
    matrix_print(&m1);

    std::cout << "\nB:\n";
    matrix_print(&m2);

    std::cout << "\nResult:\n";
    matrix_print(&m3);
  }
  if (!out.empty()) {
    if (matrix_save("../out.matrix", &m3)) {
      std::cout << "Matrix saved to " << out << '\n';
    } else {
      std::cerr << "Unable to save matrix to " << out << '\n';
    }
  }

  return 0;
}
