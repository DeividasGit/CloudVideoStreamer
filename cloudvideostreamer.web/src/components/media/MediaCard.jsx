import movieThumbnail from "../../assets/movie-1.jpg"

export default function MediaCard({ mediaContent }) {
    return (
        <div
            key={mediaContent.id}
            className="bg-white shadow rounded overflow-hidden hover:shadow-lg transition"
        >
            <img
            src={movieThumbnail} //{mediaContent.thumbnailUrl}
            alt={mediaContent.title}
            className="w-full h-48 object-cover"
            />
            <div className="p-4">
            <h2 className="text-lg font-semibold">{mediaContent.title}</h2>
            <p className="text-sm text-gray-500">
                {new Date(mediaContent.releaseDate).toLocaleDateString()}
            </p>
            </div>
        </div>     
    )
}