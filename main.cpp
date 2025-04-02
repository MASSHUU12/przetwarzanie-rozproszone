#include "matrix.h"
#include <cstdint>
#include <cstring>
#include <iostream>
#include <omp.h>
#include <unistd.h>

int main(int argc, char **argv) {
  int32_t opt, threads = 1;
  bool verbose = false;
  std::string in1 = "../m1.matrix", in2 = "../m2.matrix", out = "";

  while ((opt = getopt(argc, argv, "+a:b:o:vt:")) != -1) {
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
    case 't':
      if (optarg) {
        threads = atoi(optarg);
        if (threads <= 0) {
          fprintf(stderr, "Error: -t must be a positive integer.\n");
          return EXIT_FAILURE;
        }
      }
      break;
    default:
      std::cerr
          << "Usage: [ -a in_path ] [ -b in_path ] [ -o out_path ] [ -v ] \n";
      return 1;
    }
  }

  omp_set_num_threads(threads);

  Matrix m1, m2;
#pragma omp parallel sections
  {
#pragma omp section
    {
      m1 = matrix_read(in1);
    }
#pragma omp section
    {
      m2 = matrix_read(in2);
    }
  }

  if (m1.items == nullptr || m2.items == nullptr) {
    return 1;
  }

  double start = omp_get_wtime();
  const Matrix m3 = matrix_multiply(&m1, &m2);
  std::cout << "Multiplication time: " << omp_get_wtime() - start << "s\n";

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
    if (matrix_save(out, &m3)) {
      std::cout << "Matrix saved to " << out << '\n';
    } else {
      std::cerr << "Unable to save matrix to " << out << '\n';
    }
  }

  return 0;
}
