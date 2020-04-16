Usage:
    divine.exe [opts...] -s SOURCE -a ACTION

Options:
    -a, --action... Set action to execute (See: Actions)

    -s, --source... Set source file path or directory

    -l, --loglevel[optional]... Set verbosity level of log output

    -g, --game[optional]... Set target game when generating output

    -d, --destination[optional]... Set destination file path or directory

    -f, --packaged-path[optional]... File to extract from package

    -i, --input-format[optional]... Set input format for batch operations

    -o, --output-format[optional]... Set output format for batch operations

    -p, --package-version[optional]... Set package version

    -c, --compression-method[optional]... Set compression method

    -e, --gr2-options[optional]... Set extra options for GR2/DAE conversion

    -x, --expression[optional]... Set glob expression for extract and list actions

    --conform-path[optional]... Set conform to original path

    --use-package-name[optional]... Use package name for destination folder

    --use-regex[optional]... Use Regular Expressions for expression type

Actions:
    "batchActions":
        "extract-packages",
        "convert-models",
        "convert-resources" // Requires -i and -o
        "convert-resource"

    "graphicsActions":
        "convert-model",
        "convert-models"

    "packageActions":
        "create-package",
        "list-package",
        "extract-single-file",
        "extract-package",
        "extract-packages"

Formats:
    "dae"
    "gr2"
    "pak"
    "lsb"
    "lsf"
    "lsj"
    "lsv"
    "lsx"