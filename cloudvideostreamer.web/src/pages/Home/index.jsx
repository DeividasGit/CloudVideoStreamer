import MediaContent from "../../models/MediaContent";
import useMediaContent from "../../hooks/useMediaContent";

export default function Home() {
  const { mediaContent, loading, refresh } = useMediaContent();

  if (loading) return <div>Loading media...</div>;

  return (
      <div className="flex-1 bg-white p-4 rounded shadow">
    <h1 className="text-2xl mb-4">Home Page</h1>
    <ul>
      {mediaContent.map((item) => (
          <li key={item.id}>{item.title} - {item.description}</li>
        ))}
    </ul>
  </div>
  );
}