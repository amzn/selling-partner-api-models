#!/bin/bash
GITHUB_REPO="https://github.com/amzn/selling-partner-api-models.git"
BASE_DIR=$(cd "$(dirname "$0")" && cd .. && pwd)
CLONE_DIR="$BASE_DIR/models"

# Get swagger jar file path from the input arguments
swagger_jar_path=""
while getopts 'j:' flag; do
  case "${flag}" in
    j)  swagger_jar_path="${OPTARG}"
        ;;
    *)  echo "available options are '-j'"
        exit 1
        ;;
  esac
done

if [ ! -f "$swagger_jar_path" ]; then
    echo "option '-j <path_to_jar>' did not specify executable file. Aborted."
    exit 1
fi

##########################################################################
if [ -d "$CLONE_DIR" ]; then
  # prompt user, and if user gives "y", delete the directory.
  read -p "Found $CLONE_DIR already exists. Would you like to delete all the files under '$CLONE_DIR' and clone again? [y/n]: " confirm
  if [ "$confirm" == "y" ]; then
    # delete all the files in the directory
    rm -rf "${CLONE_DIR:?}/*"
    echo "Deleted files in $CLONE_DIR directory. Recreating $CLONE_DIR again."
    git clone "$GITHUB_REPO" "$CLONE_DIR"
  else
    echo "OK. Proceeding with the existing ${CLONE_DIR} without cloning."
  fi
else
  midir "$CLONE_DIR"
  git clone "$GITHUB_REPO" "$CLONE_DIR"
fi

##########################################################################
TEMPLATE_DIR="$BASE_DIR/src/resources/swagger-codegen/templates"
MODEL_DIR="$CLONE_DIR/models"
SDK_DIR="$BASE_DIR/sdk"
CODEGEN_CONFIG_PATH="$BASE_DIR/src/js.config.json"

MODELS=("${MODEL_DIR}"/*/*)
basePackage="js-client"

if [ -d "$SDK_DIR" ]; then
    rm -rf "${SDK_DIR:?}/*"
fi

if [ ! -d "$SDK_DIR" ]; then
  mkdir "$SDK_DIR"
fi

function get_model_name () {
  swaggerFile="$1"
  modelNameWithExtension="${swaggerFile##*/}"
  echo "${modelNameWithExtension%.*}"
}

for model in "${MODELS[@]}"
do
    modelName=$(get_model_name "$model")
    echo "model = $model"
    echo "model name = $modelName"
    java -jar "${swagger_jar_path}" generate \
        --config "${CODEGEN_CONFIG_PATH}" \
        --input-spec "${model}" \
        --lang javascript \
        --template-dir "${TEMPLATE_DIR}" \
        --output "${SDK_DIR}" \
        --invoker-package "${modelName}" \
        --api-package "${basePackage}.${modelName}.api" \
        --model-package "${basePackage}.${modelName}.model" \
        --group-id "com.amazon" \
        --artifact-id "sp-api-javascript-client"
done

echo "***********************************************************"
echo "SP-API SDK is created under ${SDK_DIR} and SDK source code "
echo "should be found ${SDK_DIR}/src/<API (with verion) name>."
echo "Please copy the SDK source DIRECTORIES (such as \"catalogItems_2022-04-01\")"
echo "you want to use to the directory \"code/javascript/src/jsdsdk.\"."