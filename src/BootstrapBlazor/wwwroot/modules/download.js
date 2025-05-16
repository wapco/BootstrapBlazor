export async function downloadFileFromStream(fileName, contentStreamReference) {
    const arrayBuffer = await contentStreamReference.arrayBuffer()
    const blob = new Blob([arrayBuffer])
    const url = URL.createObjectURL(blob)
    const anchorElement = document.createElement('a')
    anchorElement.href = url
    if (fileName == null) fileName = ""
    anchorElement.download = fileName
    anchorElement.click()
    anchorElement.remove()
    URL.revokeObjectURL(url)
}

export async function downloadFileFromUrl(fileName, url) {
    try {
        const response = await fetch(url);
        if (!response.ok) {
            alert(`下载失败：${response.statusText}`);
            return;
        }

        const blob = await response.blob();

        // 提取 Content-Disposition 中的文件名（支持 filename 和 filename*）
        const contentDisposition = response.headers.get("Content-Disposition");
        let finalFileName = fileName;

        if (contentDisposition) {
            const filenameStarMatch = contentDisposition.match(/filename\*\=UTF-8''(.+?)(;|$)/);
            const filenameMatch = contentDisposition.match(/filename="?([^";]+)"?/);

            if (filenameStarMatch) {
                finalFileName = decodeURIComponent(filenameStarMatch[1]);
            } else if (filenameMatch) {
                finalFileName = filenameMatch[1];
            }
        }

        const downloadUrl = window.URL.createObjectURL(blob);
        const a = document.createElement("a");
        a.href = downloadUrl;
        a.download = finalFileName || url.substring(url.lastIndexOf('/') + 1);
        document.body.appendChild(a);
        a.click();
        a.remove();
        window.URL.revokeObjectURL(downloadUrl);
    } catch (error) {
        console.error("文件下载出错：", error);
        alert("文件下载出错，请稍后重试。")
    }
}
