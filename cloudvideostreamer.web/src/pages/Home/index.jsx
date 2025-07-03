import useMediaContent from "../../hooks/useMediaContent";
import MediaCard from "../../components/media/MediaCard";

export default function Home() {
  const { mediaContent, loading, refresh } = useMediaContent();

  if (loading) return <div>Loading media...</div>;

  return (
    <div className="p-4 grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
      {mediaContent.map((item) => (
        <MediaCard key={item.id} mediaContent={item}/>
      ))}
    </div>
  );
}