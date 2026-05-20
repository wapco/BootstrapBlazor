#!/usr/bin/env bash
set -euo pipefail

project_name="${1:-BootstrapBlazor}"
config="${2:-Release}"

script_dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
repo_root="$(cd "${script_dir}/../.." && pwd)"
project_dir="${repo_root}/src/${project_name}"
if [[ ! -d "${project_dir}" ]]; then
  echo "Project directory not found: ${project_dir}"
  exit 1
fi

dotnet pack -c "${config}" "${project_dir}/"

src_dir="${project_dir}/bin/${config}"
dst_dir="${script_dir}"

shopt -s nullglob
packages=( "${src_dir}/${project_name}"*.nupkg )
shopt -u nullglob

if (( ${#packages[@]} == 0 )); then
  echo "No nupkg found under: ${src_dir}"
  exit 1
fi

cp -f "${packages[@]}" "${dst_dir}/"
rm -f "${packages[@]}"

cd "${dst_dir}"

nuget_source="https://nuget.aigostar.vip:5443/v3/index.json"
read -r -p "Push packages to ${nuget_source}? [y/N] " push_packages

case "${push_packages}" in
  y|Y|yes|YES|Yes|true|TRUE|True|1)
    read -r -s -p "NuGet API key: " nuget_api_key
    echo

    shopt -s nullglob
    dst_packages=( "${dst_dir}/${project_name}"*.nupkg )
    shopt -u nullglob

    for pkg in "${dst_packages[@]}"; do
      dotnet nuget push "${pkg}" --source "${nuget_source}" --api-key "${nuget_api_key}" --skip-duplicate
    done

    unset nuget_api_key
    ;;
esac

echo "Ready to DELETE ${HOME}/.nuget/packages/${project_name}"
read -r -p "Press Enter to continue..."

rm -rf "${HOME}/.nuget/packages/${project_name}"
ls -lah "${dst_dir}/${project_name}"*.nupkg
